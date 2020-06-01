import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEventType } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { RegisterUser } from '../models/RegisterUser'
import { Address } from 'src/models/Address';
import { User } from 'src/models/User';
import { Health } from 'src/models/Health';
import { BankAccount } from 'src/models/BankAccount';
import { LoginViewModel } from 'src/models/LoginViewModel';
import { strict } from 'assert';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  appUrl: string;

  constructor(private http: HttpClient) { 
    this.appUrl = "https://localhost:44322/api/account";
  }

  createUser(user: RegisterUser) {
    return this.http.post(this.appUrl+"/registration", user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  getUserProfile() : Observable<User> {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.get<User>(this.appUrl+'/profile', {headers: tokenHeader});
  }

  getUserById(userId: number): Observable<User> {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.get<User>(this.appUrl+"/"+userId,  {headers: tokenHeader});
  }

  addAddressToUser(address: Address, userId: number) {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.post(this.appUrl+"/address/add/" + userId, address, {headers: tokenHeader})
    .pipe(catchError(this.errorHandler));
  }

  editUserAddress(address: Address) {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.put(this.appUrl+"/address/edit/" + address.addressId, address, {headers: tokenHeader})
    .pipe(catchError(this.errorHandler));
  }

  addHealthToUser(health: Health, userId: number) {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.post(this.appUrl+"/health/add/" + userId, health, {headers: tokenHeader})
    .pipe(catchError(this.errorHandler));
  }

  editUserHealth(health: Health) {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.put(this.appUrl+"/health/edit/" + health.healthId, health, {headers: tokenHeader})
    .pipe(catchError(this.errorHandler));
  }

  addBankAccountToUser(bankAccount: BankAccount, userId: number) {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.post(this.appUrl+"/bankAccount/add/" + userId, bankAccount, {headers: tokenHeader})
    .pipe(catchError(this.errorHandler));
  }

  uploadImage(formData: FormData, userId) {
    this.http.post(this.appUrl+"/"+userId+"/uploadImage", formData, {reportProgress: true, observe: 'events'})
      .subscribe();
  }

  login(loginViewModel: LoginViewModel) {
    return this.http.post(this.appUrl+"/login", loginViewModel).pipe(catchError(this.errorHandler));
  }

  roleMatch(allowedRoles: Array<string>) {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole =payLoad.roles;
    
    if (payLoad == null) {
      return false;
    }
    if (typeof(userRole) == typeof("")){
      if (allowedRoles.includes(<string>userRole))
      return isMatch = true;
    }
    else {
      userRole.forEach(element => {
        if (allowedRoles.includes(element))
        return isMatch = true;
      });
    }

    return isMatch;
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Message: ${error.error}`;
    }
    return throwError(errorMessage);
  }
}
