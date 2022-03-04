import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Router} from '@angular/router';
import { Observable } from 'rxjs';
import {Respuesta} from '../../modelosGral/respuesta.modelGral'
import { ReportesModel } from './reportes.model';
import { ReporteComprasModel } from './reporteComprasModel.model';

const httpOptions = {
    headers: new HttpHeaders({
        'Contend-Type': 'application/json'
    })
  };

@Injectable({
    providedIn: 'root'
})

export class ReportesService {
private _urlReporteCompras = 'https://localhost:44383/api/Estadisticas/reporteCompras'; 

constructor(private _http: HttpClient,
    private _route: Router
    ){}

    getReporteCompras(reporteCompraDatos: ReportesModel):Observable<Respuesta>{
        let model:ReportesModel = new ReportesModel();
        
        model.tipo = reporteCompraDatos.tipo;
        model.fechaInicio = reporteCompraDatos.fechaInicio;
        model.fechaFin = reporteCompraDatos.fechaFin;
       // model.fechaInicio =Date.now();
        return this._http.post<Respuesta>(this._urlReporteCompras, model, httpOptions);
        
        //return this._http.post<Respuesta>(`${this._url}`,this._confirmarCompraModel,httpOptions);
    }
}