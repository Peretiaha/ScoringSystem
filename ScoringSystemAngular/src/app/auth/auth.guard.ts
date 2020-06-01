import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserService } from 'src/services/user.service';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    constructor(private router: Router, 
        private userService: UserService) {
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {
        if (localStorage.getItem('token') != null) {
            let roles = next.data['permittedRoles'] as Array<string>;            
            if (roles && this.userService.roleMatch(roles)) {        
                return true;
            }
            else {
                this.router.navigate(['/forbidden']);
                return false;
            }
        }
        else {
            this.router.navigate(['/account/login']);
        }
    }
}
