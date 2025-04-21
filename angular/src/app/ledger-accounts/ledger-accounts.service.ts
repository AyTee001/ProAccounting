import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateLedgerAccountInput, LedgerAccountData, UpdateLedgerAccountInput } from './models/ledger-account.models';

@Injectable({
	providedIn: 'root'
})
export class LedgerAccountsService {
	private _baseurl: string = `https://localhost:7003/api/ledgerAccounts`;

	constructor(private _httpClient: HttpClient) { }


	public getAll(): Observable<LedgerAccountData[]> {
		return this._httpClient.get<LedgerAccountData[]>(this._baseurl);
	}

	public delete(id: number): Observable<any> {
		return this._httpClient.delete(this._baseurl + `/${id}`);
	}

	public getById(id: number): Observable<LedgerAccountData> {
		return this._httpClient.get<LedgerAccountData>(this._baseurl + `/${id}`);
	}

	public create(payload: CreateLedgerAccountInput): Observable<any> {
		return this._httpClient.post(this._baseurl, payload)
	}

	public update(payload: UpdateLedgerAccountInput): Observable<any> {
		return this._httpClient.put(this._baseurl, payload)
	}
}
