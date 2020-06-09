import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginViewModel } from 'src/models/LoginViewModel';
import { TranslateService } from '@ngx-translate/core';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  formGroup: FormGroup;
  loginViewModel: LoginViewModel = new LoginViewModel();

  constructor(
    private userService: UserService,
    private popUp: MatSnackBar,
    private router: Router,
    public translate: TranslateService,
    private appComponent: AppComponent) {
      this.createForm();
  }

  ngOnInit(): void {
    
  }

  createForm() {
    this.formGroup = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]),
      password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(30)]),      
    });
  }

  submit(loginViewModel: LoginViewModel): void {
    if (this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }
    
      this.userService.login(loginViewModel).subscribe(
        (x: any) => {
          localStorage.setItem('token', x.token);
          this.popUp.open('Login successfully!', 'Ok',
            { duration: 2000, horizontalPosition: 'end', verticalPosition: 'top' });
            this.appComponent.ngOnInit();
            this.router.navigate(['/main']);
        },
        error => {
          const formControl = this.formGroup.controls.email;
          formControl.setErrors({
            badRequest: error
          });
        }
      );   
  }

  getEmailErrorMessage() {
    const nameErrors = this.formGroup.get('email').errors;
    return nameErrors.required ? 'Email is required' :
          nameErrors.minlength ? 'Email must be at least 5 characters long' :
          nameErrors.maxLength ? 'Email must be no more than 20 characters long' :
          !!this.formGroup.controls.email.errors.badRequest ? this.formGroup.controls.email.errors.badRequest :
        null;
  }

  getPasswordErrorMessage() {
    const nameErrors = this.formGroup.get('password').errors;
    return nameErrors.required ? 'Password is required' :
          nameErrors.minlength ? 'Password must be at least 6 characters long' :
          nameErrors.maxLength ? 'Password must be no more than 20 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
        }
}
