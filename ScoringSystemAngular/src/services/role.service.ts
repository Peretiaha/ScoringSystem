import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEventType } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Role } from 'src/models/role';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  appUrl: string;

  constructor(private http: HttpClient) { 
    this.appUrl = "https://localhost:44322/api/role";
  }

  fetchRoles() : Observable<Role[]> {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+ localStorage.getItem('token')});
    return this.http.get<Role[]>(this.appUrl+"/roles", {headers: tokenHeader});
  }
}
