import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateInvoiceInput, GetAllInvoiceData, InvoiceData, UpdateInvoiceInput } from './models/invoice.models';

@Injectable({
  providedIn: 'root'
})
export class InvoicesService {
	private _baseurl: string = `https://localhost:7003/api/invoices`;

	constructor(private _httpClient: HttpClient) { }


	public getAll(): Observable<GetAllInvoiceData[]> {
		return this._httpClient.get<GetAllInvoiceData[]>(this._baseurl);
	}

	public delete(id: number): Observable<any> {
		return this._httpClient.delete(this._baseurl + `/${id}`);
	}

	public getById(id: number): Observable<InvoiceData> {
		return this._httpClient.get<InvoiceData>(this._baseurl + `/${id}`);
	}

	public create(payload: CreateInvoiceInput): Observable<any> {
		return this._httpClient.post(this._baseurl, payload)
	}

	public update(payload: UpdateInvoiceInput): Observable<any> {
		return this._httpClient.put(this._baseurl, payload)
	}

}
