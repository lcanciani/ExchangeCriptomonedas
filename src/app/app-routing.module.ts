import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import {LoginComponent} from './aplicacion/login/login.component';
import {AuthGuard} from './aplicacion/login/auth.guard';
import {AplicacionComponent} from './aplicacion/aplicacion.component'
import { VentaComponent } from './aplicacion/venta/venta.component';
import { ConfigurarVentaComponent } from './aplicacion/venta/configurar-venta/configurar-venta.component';
import { ConfirmarVentaComponent } from './aplicacion/venta/confirmar-venta/confirmar-venta.component';

const routes: Routes = [
  {path: 'comp', component: AplicacionComponent, canActivate: [AuthGuard]},
  {path: 'login',component: LoginComponent },
  {path: 'compraVenta',component:VentaComponent},
  {path: 'configurarVenta', component: ConfigurarVentaComponent},
  {path: 'confirmarVenta', component: ConfirmarVentaComponent},
  {path: '',component: LoginComponent },
{path: '**',component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
