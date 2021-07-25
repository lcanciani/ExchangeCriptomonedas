import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient } from '@angular/common/http';



@Injectable({
  providedIn: 'root'
})
export class DashboardService{
 
    constructor( private _http: HttpClient) { }
    private _url: string = 'https://localhost:44383/api/Dashboard';
    private _urlTipoMovimiento: string = 'https://localhost:44383/api/TipoMovimiento';

    getDataForDashboard(): Observable<Respuesta>{
      return this._http.get<Respuesta>(this._url);
    }
    
    getTipoMovimientoById(id: number):Observable<Respuesta>{
      return this._http.get<Respuesta>(`${this._urlTipoMovimiento}/${id}`);
    }
    
}