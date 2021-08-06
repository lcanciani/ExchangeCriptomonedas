import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { MailRequest } from './mailRequest.model';
import { Observable } from 'rxjs';
import { Respuesta } from '../../modelosGral/respuesta.modelGral';

const httpOptions = {
    headers: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
  };


@Injectable({
    providedIn: 'root'
})
export class MailService{
    
    private _url: string = 'https://localhost:44383/api/Mail/send';

    constructor(private _http: HttpClient){}

    registrarMail(mailDatos: MailRequest): Observable<Respuesta>{
        return this._http.post<Respuesta>(this._url,mailDatos, httpOptions)
    }

}