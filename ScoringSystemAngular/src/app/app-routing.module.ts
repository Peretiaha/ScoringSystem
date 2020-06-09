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
import { UsersListComponent } from './users-list/users-list.component';
import { ChangeRoleComponent } from './change-role/change-role.component';
import { CustomerComponent } from './customer/customer.component';


const routes: Routes = [
  { path: 'banks', component: BankComponent, canActivate:[AuthGuard], data: { permittedRoles:['Admin'] }},
  { path: 'account/:id/change-role', component: ChangeRoleComponent, canActivate:[AuthGuard], data: { permittedRoles:['Admin'] }},
  { path: 'account/customers', component: CustomerComponent, canActivate:[AuthGuard], data: { permittedRoles:['Admin'] }},
  { path: '', redirectTo: '/account/login', pathMatch: 'full' },
  { path: 'account/', component: UserComponent },
  { path: 'account/registration', component: RegistrationComponent },
  { path: 'account/:id/address/add', component: AddressComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/:id/health/add', component: HealthComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/:id/bankAccount/add', component: BankAccountComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/profile', component: UserProfileComponent, canActivate:[AuthGuard], data: { permittedRoles:['Customer'] }},
  { path: 'account/login', component: LoginComponent},
  { path: 'account/users', component: UsersListComponent, canActivate:[AuthGuard], data: { permittedRoles:['Manager'] }},
  { path: 'forbidden', component: ForbiddenComponent},
  { path: 'main', component: MainComponent, canActivate:[AuthGuard], data: { permittedRoles:['Admin', 'Manager'] } },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
