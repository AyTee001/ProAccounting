import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogModule, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { LedgerAccountsService } from '../../ledger-accounts.service';
import { CreateLedgerAccountInput, LedgerType, ledgerTypesStringMapping, UpdateLedgerAccountInput } from '../../models/ledger-account.models';
import { take } from 'rxjs';
import { MatSelectModule } from '@angular/material/select'

interface EditLedgerAccountDialogData {
	id?: number
}

@Component({
	selector: 'app-edit-ledger-account-dialog',
	imports: [MatDialogModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		MatDialogTitle,
		MatDialogContent,
		MatDialogActions,
		MatSelectModule],
	templateUrl: './edit-ledger-account-dialog.component.html',
	styleUrl: './edit-ledger-account-dialog.component.scss'
})
export class EditLedgerAccountDialogComponent {
	public readonly dialogRef = inject(MatDialogRef<EditLedgerAccountDialogComponent>);
	public readonly data = inject<EditLedgerAccountDialogData>(MAT_DIALOG_DATA);
	public title = "";
	
	public readonly ledgerTypeOptions = ledgerTypesStringMapping;

	public ledgerForm = new FormGroup({
		id: new FormControl(),
		name: new FormControl('', Validators.required),
		code: new FormControl<number | null>(null, Validators.required),
		ledgerType: new FormControl<LedgerType>(LedgerType.Asset, Validators.required)
	});

	constructor(private _ledgerAccountsService: LedgerAccountsService) {
		const id = this.data?.id;
		this.title = id ? "Edit ledger account" : "Create ledger account";

		if (id != null) {
			_ledgerAccountsService.getById(id).subscribe(client => {
				console.log(client)
				this.ledgerForm.patchValue(client);
			});

			this.ledgerForm.get('code')?.disable();
			this.ledgerForm.get('ledgerType')?.disable();
		}
	}

	closeDialog() {
		this.dialogRef.close();
	}

	save() {
		const formValue = this.ledgerForm.getRawValue();


		if (!formValue.id) {
			const clientData: CreateLedgerAccountInput = {
				name: formValue.name ?? '',
				code: formValue.code!,
				ledgerType: formValue.ledgerType!
			}

			this._ledgerAccountsService.create(clientData)
				.pipe(take(1))
				.subscribe({
					next: () => {
						this.dialogRef.close(true);
					}
				});
		}
		else {
			const clientData: UpdateLedgerAccountInput = {
				id: formValue.id,
				name: formValue.name ?? ''
			}

			this._ledgerAccountsService.update(clientData)
				.pipe(take(1))
				.subscribe({
					next: () => {
						this.dialogRef.close(true);
					}
				});
		}
	}

}
