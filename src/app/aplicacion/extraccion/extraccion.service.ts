import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from '../../modelosGral/respuesta.modelGral';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ExtraccionModel } from './extraccion.model';
const httpOptions = {
  headers: new HttpHeaders({
      'Contend-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ExtraccionService{
    depositoModel: ExtraccionModel;
    constructor( private _http: HttpClient) { }
    private _url: string = 'https://localhost:44383/registrarExtraccion';
    private _urlDatos: string = 'https://localhost:44383/datosExtraccion';

    insertarExtraccion(model: ExtraccionModel):Observable<Respuesta> {
        return this._http.post<Respuesta>(this._url,model,httpOptions);
    }

    getDatosExtraccion(idUsuario: number):Observable<Respuesta>{
      return this._http.post<Respuesta>(this._urlDatos,idUsuario,httpOptions); 
    }
}