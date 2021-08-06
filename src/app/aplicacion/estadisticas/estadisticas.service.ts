import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Router} from '@angular/router';
import { Observable } from 'rxjs';
import {Respuesta} from '../../modelosGral/respuesta.modelGral'

const httpOptions = {
    headers: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
  };

@Injectable({
    providedIn: 'root'
})

export class EstadisticasService {
    private _url = 'https://localhost:44383/api/Estadisticas/criptomonedaEstadisticas';
    constructor(private _http: HttpClient,
                private _route: Router
                ){}
    getCriptoConsAgrup(): Observable<Respuesta>{
        return this._http.get<Respuesta>(this._url, httpOptions);
    }
    



}