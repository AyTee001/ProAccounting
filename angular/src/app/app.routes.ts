import { Routes } from '@angular/router';
import { ClientsComponent } from './clients/clients.component';
import { LedgerAccountsComponent } from './ledger-accounts/ledger-accounts.component';

export const routes: Routes = [
    { path: 'clients', pathMatch: 'full', component: ClientsComponent },
    { path: 'ledgerAccounts', pathMatch: 'full', component: LedgerAccountsComponent },
    { path: '', pathMatch: 'full', redirectTo: 'dashboard' }
];
