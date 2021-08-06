import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import {LoginComponent} from './aplicacion/login/login.component';
import {AuthGuard} from './aplicacion/login/auth.guard';
import {AplicacionComponent} from './aplicacion/aplicacion.component'
import { RegistrarUsuarioComponent } from './aplicacion/usuario/registrar-usuario/registrar-usuario.component';


const routes: Routes = [
  {path: 'comp', component: AplicacionComponent,loadChildren:()=>import( './aplicacion/aplicacion.module').then(m=>m.AplicacionModule)},
  {path: 'login',component: LoginComponent },
  //{path: 'compraVenta',component:VentaComponent},
  //{path: 'configurarVenta', component: ConfigurarVentaComponent},
  //{path: 'confirmarVenta', component: ConfirmarVentaComponent},
  //{path: 'criptomonedas', component:CriptomonedasComponent},
  {path: 'registrarUsuario', component: RegistrarUsuarioComponent},
  {path: '',component: LoginComponent },
{path: '**',component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
