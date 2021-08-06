import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from '../../modelosGral/respuesta.modelGral';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
    headers: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})
export class BancosService{

    private _url: string = 'https://localhost:44383/api/Bancos';
    constructor(private _http: HttpClient){}

    getBancos():Observable<Respuesta>{
        return this._http.get<Respuesta>(this._url, httpOptions);
    }
    
}
