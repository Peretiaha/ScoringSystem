import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Bank } from '../models/bank'

@Injectable({
  providedIn: 'root'
})
export class BankService {

  appUrl: string;

  constructor(private http: HttpClient) { 
    this.appUrl = "https://localhost:44322/api/bank";
  }

  fetchBanks() : Observable<Bank[]> {
    return this.http.get<Bank[]>(this.appUrl);
  }

  deleteById(bankId: number) : Observable<Bank>{
    return this.http.delete<Bank>(this.appUrl+"/"+ bankId);
  }

  createBank(bank: Bank) {
    return this.http.post(this.appUrl+"/add", bank).pipe();
  }

  editBank(bank: Bank) {
    return this.http.put(this.appUrl + "/edit/" + bank.bankId, bank).pipe(catchError(this.errorHandler));
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
