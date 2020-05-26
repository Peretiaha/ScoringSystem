import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BankComponent } from './bank/bank.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './registration/registration.component';


const routes: Routes = [
  { path: 'banks', component: BankComponent},
  { path: '', redirectTo: '/banks', pathMatch: 'full' },
  { path: 'account/', component: UserComponent },
  { path: 'account/registration', component: RegistrationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
