import { Component, OnInit } from '@angular/core';
import { ClientsService } from './clients.service';
import { ClientData } from './models/client.models';
import { take } from 'rxjs';
import { MatTableModule, MatTableDataSource } from '@angular/material/table'
import { MatIconModule } from '@angular/material/icon'
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { EditClientComponent } from './dialogs/edit-client/edit-client.component';

@Component({
	selector: 'clients',
	imports: [MatTableModule, MatIconModule, MatButtonModule],
	templateUrl: './clients.component.html',
	styleUrl: './clients.component.scss'
})
export class ClientsComponent implements OnInit {
	public clientsSource: MatTableDataSource<ClientData> = new MatTableDataSource<ClientData>();

	public displayedColumns = ['action', 'name', 'email', 'address', 'phoneNumber'];

	constructor(private _clientService: ClientsService,
		private _dialog: MatDialog
	) {

	}

	ngOnInit(): void {
		this.getClients();
	}

	private getClients(){
		this._clientService.getAll()
		.pipe(take(1))
		.subscribe({
			next: (res) => {
				this.clientsSource.data = res
			}
		})
	}

	deleteClient(id: number){
		this._clientService.delete(id)
		.pipe(take(1))
		.subscribe({
				next: () => {
				this.getClients();
			}
		});
	}

	editClient(id: number){
		let dialogRef = this._dialog.open(EditClientComponent, {
			width: '500px',
			height: '470px',
			data: {
				id: id
			}
		})

		dialogRef.afterClosed()
		.subscribe(refresh => {
			if(refresh) {
				this.getClients();
			}
		})

	}

	createClient() {
		let dialogRef = this._dialog.open(EditClientComponent, {
			width: '500px',
  			height: '470px',
			data: {
				id: undefined
			}
		})

		dialogRef.afterClosed()
		.subscribe(refresh => {
			if(refresh) {
				this.getClients();
			}
		})
	}
}
