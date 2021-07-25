import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient } from '@angular/common/http';
import { DepositoModel } from './deposito.model';


@Injectable({
  providedIn: 'root'
})
export class DepositoService{
    depositoModel: DepositoModel;
    constructor( private _http: HttpClient) { }
    private _url: string = 'https://localhost:44383/api/Depositos';

    insertarDeposito(model: DepositoModel):Observable<Respuesta> {
        return this._http.post<Respuesta>(this._url,model);
    }
}