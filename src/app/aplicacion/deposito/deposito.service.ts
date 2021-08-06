import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DepositoModel } from './deposito.model';

const httpOptions = {
  headers: new HttpHeaders({
      'Contend-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class DepositoService{
    depositoModel: DepositoModel;
    constructor( private _http: HttpClient) { }
    private _url: string = 'https://localhost:44383/api/Depositos';

    insertarDeposito(model: DepositoModel):Observable<Respuesta> {
        return this._http.post<Respuesta>(this._url,model, httpOptions);
    }
}