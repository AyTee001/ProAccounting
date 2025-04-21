import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { LedgerAccountsService } from './ledger-accounts.service';
import { LedgerAccountData, LedgerType, ledgerTypesStringMapping } from './models/ledger-account.models';
import { take } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { EditLedgerAccountDialogComponent } from './dialogs/edit-ledger-account-dialog/edit-ledger-account-dialog.component';

@Component({
	selector: 'app-ledger-accounts',
	imports: [MatTableModule, MatIconModule, MatButtonModule],
	templateUrl: './ledger-accounts.component.html',
	styleUrl: './ledger-accounts.component.scss'
})
export class LedgerAccountsComponent implements OnInit {
	public ledgerAccountsSource: MatTableDataSource<LedgerAccountData> = new MatTableDataSource<LedgerAccountData>();

	public displayedColumns = ['action', 'code', 'name', 'ledgerType'];

	public getLedgerTypeNameByLedgerType(ledgerType: LedgerType){
		return ledgerTypesStringMapping?.find(x => x.value === ledgerType)?.label ?? 'Undefined';
	}

	constructor(private _ledgerAccountsService: LedgerAccountsService,
		private _dialog: MatDialog
	) {

	}

	ngOnInit(): void {
		this.getLedgerAccounts();
	}

	private getLedgerAccounts() {
		this._ledgerAccountsService.getAll()
			.pipe(take(1))
			.subscribe({
				next: (res) => {
					this.ledgerAccountsSource.data = res
				}
			})
	}

	deleteLedgerAccount(id: number) {
		this._ledgerAccountsService.delete(id)
			.pipe(take(1))
			.subscribe({
				next: () => {
					this.getLedgerAccounts();
				}
			});
	}

	editLedgerAccount(id: number) {
		let dialogRef = this._dialog.open(EditLedgerAccountDialogComponent, {
			width: '500px',
			height: '420px',
			data: {
				id: id
			}
		})

		dialogRef.afterClosed()
			.subscribe(refresh => {
				if (refresh) {
					this.getLedgerAccounts();
				}
			})

	}

	createLedgerAccount() {
		let dialogRef = this._dialog.open(EditLedgerAccountDialogComponent, {
			width: '500px',
			height: '420px',
			data: {
				id: undefined
			}
		})

		dialogRef.afterClosed()
			.subscribe(refresh => {
				if (refresh) {
					this.getLedgerAccounts();
				}
			})
	}

}
