
import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Respuesta} from '../../modelosGral/respuesta.modelGral';
import { RegistrarUsuarioModel } from './registrar-usuario/registrarUsuario.model';

const httpOptions = {
    headers: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
  };

@Injectable({
    providedIn:'root'
})
export class UsuarioService{
private _url: string = 'https://localhost:44383/api/Usuarios';

    constructor(private _http: HttpClient){}
    
    registrarUsuario(uModel: RegistrarUsuarioModel): Observable<Respuesta>{
        return this._http.post<Respuesta>(this._url, uModel, httpOptions);
    }
    getSaldoFiat(id: number):Observable<Respuesta>{
        return this._http.get<Respuesta>(`${this._url}/${id}`, httpOptions)
    }

    getUsuarios(): Observable<Respuesta>{
        return this._http.get<Respuesta>(this._url);
    }
}