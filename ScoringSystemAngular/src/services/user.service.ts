import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEventType } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { RegisterUser } from '../models/RegisterUser'
import { Address } from 'src/models/Address';
import { User } from 'src/models/User';

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

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(this.appUrl+"/"+userId);
  }

  addAddressToUser(address: Address, userId: number) {
    return this.http.post(this.appUrl+"/address/add/" + userId, address)
    .pipe(catchError(this.errorHandler));
  }

  editUserAddress(address: Address) {
    return this.http.put(this.appUrl+"/address/edit/" + address.addressId, address)
    .pipe(catchError(this.errorHandler));
  }

  uploadImage(formData: FormData, userId) {
    this.http.post(this.appUrl+"/"+userId+"/uploadImage", formData, {reportProgress: true, observe: 'events'})
      .subscribe();
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
