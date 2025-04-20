import { Routes } from '@angular/router';
import { ClientsComponent } from './clients/clients.component';

export const routes: Routes = [
    { path: 'clients', pathMatch: 'full', component: ClientsComponent},
    { path: '', pathMatch: 'full', redirectTo: 'dashboard' }
];
