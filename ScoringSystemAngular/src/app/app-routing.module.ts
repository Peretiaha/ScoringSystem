import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BankComponent } from './bank/bank.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './registration/registration.component';
import { AddressComponent } from './address/address.component';
import { HealthComponent } from './health/health.component';
import { BankAccount } from 'src/models/BankAccount';
import { BankAccountComponent } from './bank-account/bank-account.component';


const routes: Routes = [
  { path: 'banks', component: BankComponent},
  { path: '', redirectTo: '/banks', pathMatch: 'full' },
  { path: 'account/', component: UserComponent },
  { path: 'account/registration', component: RegistrationComponent },
  { path: 'account/:id/address/add', component: AddressComponent},
  { path: 'account/:id/health/add', component: HealthComponent},
  { path: 'account/:id/bankAccount/add', component: BankAccountComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
