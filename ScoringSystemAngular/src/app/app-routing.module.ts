import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BankComponent } from './bank/bank.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './registration/registration.component';
import { AddressComponent } from './address/address.component';
import { HealthComponent } from './health/health.component';
import { BankAccount } from 'src/models/BankAccount';
import { BankAccountComponent } from './bank-account/bank-account.component';
import { LoginComponent } from './login/login.component';
import { ForbiddenComponent } from './login/forbidden/forbidden.component';
import { AuthGuard } from './auth/auth.guard';
import { MainComponent } from './main/main.component';
import { UserProfileComponent } from './user-profile/user-profile.component';


const routes: Routes = [
  { path: 'banks', component: BankComponent, canActivate:[AuthGuard], data: { permittedRoles:['Admin'] }},
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'account/', component: UserComponent },
  { path: 'account/registration', component: RegistrationComponent },
  { path: 'account/:id/address/add', component: AddressComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/:id/health/add', component: HealthComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/:id/bankAccount/add', component: BankAccountComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/profile', component: UserProfileComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/login', component: LoginComponent},
  { path: 'forbidden', component: ForbiddenComponent},
  { path: 'main', component: MainComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
