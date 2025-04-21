import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { GetAllInvoiceData } from './models/invoice.models';
import { InvoicesService } from './invoices.service';
import { MatDialog } from '@angular/material/dialog';
import { take } from 'rxjs';
import { EditInvoiceComponent } from './dialogs/edit-invoice/edit-invoice.component';

@Component({
	selector: 'app-invoices',
	imports: [MatTableModule, MatIconModule, MatButtonModule],
	templateUrl: './invoices.component.html',
	styleUrl: './invoices.component.scss'
})
export class InvoicesComponent {
	public clientsSource: MatTableDataSource<GetAllInvoiceData> = new MatTableDataSource<GetAllInvoiceData>();

	public displayedColumns = ['id', 'client', 'date', 'dueDate', 'total', 'status'];

	constructor(private _invocesService: InvoicesService,
		private _dialog: MatDialog
	) {

	}

	ngOnInit(): void {
		this.getInvoices();
	}

	private getInvoices() {
		this._invocesService.getAll()
			.pipe(take(1))
			.subscribe({
				next: (res) => {
					this.clientsSource.data = res
				}
			})
	}

	deleteInvoice(id: number) {
		this._invocesService.delete(id)
			.pipe(take(1))
			.subscribe({
				next: () => {
					this.getInvoices();
				}
			});
	}

	editInvoice(id: number) {
		let dialogRef = this._dialog.open(EditInvoiceComponent, {
			width: '80vw',
			height: '80vh',
			data: {
				id: id
			}
		})

		dialogRef.afterClosed()
			.subscribe(refresh => {
				if (refresh) {
					this.getInvoices();
				}
			})

	}

	createInvoice() {
		let dialogRef = this._dialog.open(EditInvoiceComponent, {
			maxWidth: '90vw',
			maxHeight: '90vh',
			height: '100%',
			width: '100%',
			data: {
				id: undefined
			}
		})

		dialogRef.afterClosed()
			.subscribe(refresh => {
				if (refresh) {
					this.getInvoices();
				}
			})
	}

}
