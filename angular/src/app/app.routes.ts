import { Routes } from '@angular/router';
import { ClientsComponent } from './clients/clients.component';
import { LedgerAccountsComponent } from './ledger-accounts/ledger-accounts.component';
import { InvoicesComponent } from './invoices/invoices.component';

export const routes: Routes = [
    { path: 'clients', pathMatch: 'full', component: ClientsComponent },
    { path: 'ledgerAccounts', pathMatch: 'full', component: LedgerAccountsComponent },
    { path: 'invoices', pathMatch: 'full', component: InvoicesComponent },
    { path: '', pathMatch: 'full', redirectTo: 'dashboard' }
];
