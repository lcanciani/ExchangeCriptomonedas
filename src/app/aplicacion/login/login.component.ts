import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { RespuestaAuth } from './respuestaAuth.model';
import { Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators  } from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm : FormGroup;
  token: string;
  respu : RespuestaAuth = null;
  hide = true;
  constructor( private _loginService: LoginService,
               private _route: Router,
               private _formBuilder: FormBuilder,
               private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      email:['', [Validators.required,Validators.email]],
      password:['', Validators.required]
    })

  }

  getUserToken(){
    
    this._loginService.getToken(this.loginForm.value).subscribe(resp => {
      if(resp.exito ===1){
       // console.log(resp.data.token)
        this._route.navigate(['comp/dashboard']);
      }
      else{
        this._snackBar.open('Usuario o contraseña incorrecto','',{
          duration:2000,
          verticalPosition:'top'
        })
        this.loginForm.reset();
      }
    })
  }

 
}
