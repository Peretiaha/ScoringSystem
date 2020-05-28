import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BankComponent } from './bank/bank.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './registration/registration.component';
import { AddressComponent } from './address/address.component';


const routes: Routes = [
  { path: 'banks', component: BankComponent},
  { path: '', redirectTo: '/banks', pathMatch: 'full' },
  { path: 'account/', component: UserComponent },
  { path: 'account/registration', component: RegistrationComponent },
  { path: 'account/:id/address/add', component: AddressComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
