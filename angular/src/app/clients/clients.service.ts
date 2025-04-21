import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientData, CreateClientInput, UpdateClientInput } from './models/client.models';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class ClientsService {
	private _baseurl: string = `https://localhost:7003/api/clients`;

	constructor(private _httpClient: HttpClient) { }


	public getAll(): Observable<ClientData[]> {
		return this._httpClient.get<ClientData[]>(this._baseurl);
	}

	public delete(id: number): Observable<any> {
		return this._httpClient.delete(this._baseurl + `/${id}`);
	}

	public getById(id: number): Observable<ClientData> {
		return this._httpClient.get<ClientData>(this._baseurl + `/${id}`);
	}

	public create(payload: CreateClientInput): Observable<any> {
		return this._httpClient.post(this._baseurl, payload)
	}

	public update(payload: UpdateClientInput): Observable<any> {
		return this._httpClient.put(this._baseurl, payload)
	}
}
