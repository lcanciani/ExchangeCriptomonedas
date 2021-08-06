import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
      'Contend-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class DashboardService{
 
    constructor( private _http: HttpClient) { }
    private _url: string = 'https://localhost:44383/api/Dashboard';
    private _urlTipoMovimiento: string = 'https://localhost:44383/api/TipoMovimiento';
    private _urlMovimientosUsuario: string = 'https://localhost:44383/api/Dashboard/movimientos';

    getDataForDashboard(id: number): Observable<Respuesta>{
      return this._http.get<Respuesta>(`${this._url}/${id}`,httpOptions);
    }
    
    getTipoMovimientoById(id: number):Observable<Respuesta>{
      return this._http.get<Respuesta>(`${this._urlTipoMovimiento}/${id}`,httpOptions);
    }

    getMovimientosUsuario(id: number):Observable<Respuesta>{
      return this._http.get<Respuesta>(`${this._urlMovimientosUsuario}/${id}`,httpOptions);
    }
}