import {CriptomonedaModel} from '../criptomonedas/criptomoneda.model';
import {BehaviorSubject, Observable} from 'rxjs';
import {Injectable,OnInit} from '@angular/core';
import {CriptomonedasService} from '../criptomonedas/criptomonedas.service'
import {ConfiguracionVenta} from './configurar-venta/cofiguracionVenta.model';
import { Respuesta } from 'src/app/modelosGral/respuesta.modelGral';
import { HttpClient} from "@angular/common/http";
import { Router } from '@angular/router';
import { ConfirmarVentaComponent } from './confirmar-venta/confirmar-venta.component';
import { ConfirmarVentaModel } from './confirmar-venta/confirmarVenta.model';

@Injectable({
    providedIn: 'root'
})
export class VentaService implements OnInit{
private    _subjectCriptoVenta=new BehaviorSubject<Respuesta>({}as any);
private _url: string = "https://localhost:44383/api/Compra";
confirmarVentaMocel: ConfirmarVentaModel;
constructor(private _criptoService:CriptomonedasService,
            private _http: HttpClient,
            private _route: Router){}

ngOnInit(){
    
}

getCriptoVentaById(idCripto: number): Observable<Respuesta>{
return this._http.get<Respuesta>(`${this._url}/${idCripto}`);
}

configurarVenta(idCripto: number){
    this.getCriptoVentaById(idCripto).subscribe(resp =>{
        
       this._subjectCriptoVenta.next(resp);
       this._route.navigate(['configurarVenta']);
    })
    
}

getCriptoAConfigurarVenta():Observable<Respuesta>{
    return this._subjectCriptoVenta.asObservable();

}
insertarVenta():Observable<Respuesta>{
 return this._http.post<Respuesta>(`${this._url}`,this.confirmarVentaMocel);
}
}