import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormControl, FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { take } from 'rxjs';
import { InvoicesService } from '../../invoices.service';
import { CreateInvoiceInput, UpdateInvoiceInput } from '../../models/invoice.models';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, provideNativeDateAdapter } from '@angular/material/core';
import { ClientData } from '../../../clients/models/client.models';
import { ClientsService } from '../../../clients/clients.service';

interface EditEnvoiceDialogData {
	id?: number | null;
}

@Component({
	selector: 'app-edit-invoice',
	imports: [MatDialogModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		MatDialogTitle,
		MatDialogContent,
		MatDialogActions,
		MatIconModule,
		MatTableModule,
		MatSelectModule,
		MatDatepickerModule,
		MatNativeDateModule,
	],
	providers: [provideNativeDateAdapter()],
	templateUrl: './edit-invoice.component.html',
	styleUrl: './edit-invoice.component.scss'
})
export class EditInvoiceComponent {
	public readonly dialogRef = inject(MatDialogRef<EditInvoiceComponent>);
	public readonly data = inject<EditEnvoiceDialogData>(MAT_DIALOG_DATA);
	private readonly _cdr = inject(ChangeDetectorRef);
	public title = "";

	public dataSource = new MatTableDataSource<FormGroup>();

	public displayedColumns = ['productName', 'description', 'quantity', 'unitPrice', 'actions'];
	public clients: ClientData[] = [];
	public invoiceForm!: FormGroup;

	constructor(private _invoicesService: InvoicesService,
		private _clientsService: ClientsService,
		private _fb: FormBuilder
	) {
		const id = this.data?.id;
		this.title = id ? "Edit invoice" : "Create invoice";

		this.invoiceForm = this._fb.group({
			id: [null],
			clientId: [null, Validators.required],
			date: [null, Validators.required],
			dueDate: [null, Validators.required],
			invoiceItems: this._fb.array([])
		});


		if (id != null) {
			_invoicesService.getById(id).subscribe(invoice => {
				this.invoiceForm.patchValue(invoice);
			});
		}

		this._clientsService.getAll()
			.pipe(take(1))
			.subscribe({
				next: (res) => {
					this.clients = res;
				}
			})
	}

	createInvoiceItem(): FormGroup {
		return this._fb.group({
			productName: ['', Validators.required],
			description: [''],
			quantity: [1, [Validators.required, Validators.min(1)]],
			unitPrice: [0, [Validators.required, Validators.min(0)]]
		});
	}

	get invoiceItems(): FormArray {
		return this.invoiceForm.get('invoiceItems') as FormArray;
	}

	addInvoiceItem() {
		const item = this.createInvoiceItem();
		this.invoiceItems.push(item);
		this.updateTableData();
	}

	removeInvoiceItem(index: number) {
		this.invoiceItems.removeAt(index);
		this.updateTableData();
	}

	private updateTableData() {
		this.dataSource.data = this.invoiceItems.controls as FormGroup[];
	}

	closeDialog() {
		this.dialogRef.close();
	}

	save() {
		const formValue = this.invoiceForm.getRawValue();


		if (!formValue.id) {
			const invoiceData: CreateInvoiceInput = {
				date: formValue.date,
				dueDate: formValue.dueDate,
				clientId: formValue.clientId,
				invoiceItems: formValue.invoiceItems
			}

			this._invoicesService.create(invoiceData)
				.pipe(take(1))
				.subscribe({
					next: () => {
						this.dialogRef.close(true);
					}
				});
		}
		else {
			const invoiceData: UpdateInvoiceInput = {
				id: formValue.id,
				date: formValue.date,
				dueDate: formValue.dueDate,
				invoiceItems: formValue.invoiceItems
			}

			this._invoicesService.update(invoiceData)
				.pipe(take(1))
				.subscribe({
					next: () => {
						this.dialogRef.close(true);
					}
				});
		}
	}
}
