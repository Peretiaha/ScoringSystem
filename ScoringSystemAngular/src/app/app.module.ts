import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BankComponent } from './bank/bank.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import {CdkTableModule} from '@angular/cdk/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { DeleteModalComponent } from './delete-modal/delete-modal.component';
import { BankModalComponent } from './bank/bank-modal/bank-modal.component';
import {ReactiveFormsModule, FormsModule } from '@angular/forms';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './registration/registration.component';
import { AddressComponent } from './address/address.component';
import { HealthComponent } from './health/health.component';
import { BankAccountComponent } from './bank-account/bank-account.component';

@NgModule({
  declarations: [
    AppComponent,
    BankComponent,
    DeleteModalComponent,
    BankModalComponent,
    UserComponent,
    RegistrationComponent,
    AddressComponent,
    HealthComponent,
    BankAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    CdkTableModule,
    ReactiveFormsModule,
    FormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
