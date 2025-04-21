import { Component, inject } from '@angular/core';
import { FormGroup, FormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
	MAT_DIALOG_DATA,
	MatDialog,
	MatDialogActions,
	MatDialogClose,
	MatDialogContent,
	MatDialogRef,
	MatDialogTitle,
	MatDialogModule
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ClientsService } from '../../clients.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ClientData, CreateClientInput, UpdateClientInput } from '../../models/client.models';
import { take } from 'rxjs';

interface EditClientDialogData {
	id?: number
}

@Component({
	selector: 'app-edit-client',
	imports: [MatDialogModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		MatDialogTitle,
		MatDialogContent,
		MatDialogActions],
	templateUrl: './edit-client.component.html',
	styleUrl: './edit-client.component.scss'
})
export class EditClientComponent {
	public readonly dialogRef = inject(MatDialogRef<EditClientComponent>);
	public readonly data = inject<EditClientDialogData>(MAT_DIALOG_DATA);
	public title = "";

	public clientForm = new FormGroup({
		id: new FormControl(),
		name: new FormControl('', Validators.required),
		email: new FormControl('', Validators.required),
		phoneNumber: new FormControl(''),
		address: new FormControl(''),
	});


	constructor(private _clientsService: ClientsService) {
		const id = this.data?.id;
		this.title = id ? "Edit client" : "Create client";

		if (id != null) {
			_clientsService.getById(id).subscribe(client => {
				console.log(client)
				this.clientForm.patchValue(client);
			});
		}
	}

	closeDialog() {
		this.dialogRef.close();
	}

	save() {
		const formValue = this.clientForm.getRawValue();


		if(!formValue.id) {
			const clientData: CreateClientInput = {
				name: formValue.name ?? '',
				phoneNumber: formValue.phoneNumber,
				email: formValue.email ?? '',
				address: formValue.address
			} 
	
			this._clientsService.create(clientData)
			.pipe(take(1))
			.subscribe({
				next: () => {
					this.dialogRef.close(true);
				}
			});
		}
		else {
			const clientData: UpdateClientInput = {
				id: formValue.id,
				name: formValue.name ?? '',
				phoneNumber: formValue.phoneNumber,
				email: formValue.email ?? '',
				address: formValue.address
			} 
	
			this._clientsService.update(clientData)
			.pipe(take(1))
			.subscribe({
				next: () => {
					this.dialogRef.close(true);
				}
			});
		}
	}
}
