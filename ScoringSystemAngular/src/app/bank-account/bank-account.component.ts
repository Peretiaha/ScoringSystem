import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BankAccount } from 'src/models/BankAccount';
import { User } from 'src/models/User';
import { Bank } from 'src/models/bank';
import { BankService } from 'src/services/bank.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-bank-account',
  templateUrl: './bank-account.component.html',
  styleUrls: ['./bank-account.component.scss']
})
export class BankAccountComponent implements OnInit {

  user: User = new User();
  userId: number;
  formGroup: FormGroup;
  bankAccount: BankAccount = new BankAccount();
  userName: string;
  bankName: string;
  banks: Bank[];

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private bankService: BankService,
    private popUp: MatSnackBar,
    private router: Router,
    public translate: TranslateService) {
    this.createForm();
  }

  ngOnInit(): void {
    this.bankService.fetchBanks().subscribe(x=> this.banks = <Bank[]>x);
    const id = 'id';
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));    
    this.route.params.subscribe(params => this.userId = params[id]);
    this.userService.getUserProfile().subscribe(x => {
      this.userName = x.name;
    });

  }

  createForm() {
    this.formGroup = new FormGroup({
      cardNumber: new FormControl('', [Validators.required, Validators.min(999999999999999), Validators.max(9999999999999999)]),
      debt: new FormControl('', ),
      credit: new FormControl('', ),     
      bankName: new FormControl('',),
    });
  }

  onCancelClick(): void {

  }

  submit(bankAccount: BankAccount): void {
    if (this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }    
    bankAccount.bank = this.banks.find(x=>x.name == this.bankName)
    this.userService.addBankAccountToUser(bankAccount, this.userId).subscribe(
        x => {
          this.popUp.open('Bank Account successfully added to user data!', 'Ok',
            { duration: 2000, horizontalPosition: 'end', verticalPosition: 'top' });
            this.router.navigate(['/main']);
        },
        error => {
          const formControl = this.formGroup.controls.name;
          formControl.setErrors({
            isbnError: error
          });
        }
      );    
  }

  getCardErrorMessage() {
    return this.formGroup.controls.cardNumber.hasError('required') ? 'Title is required' :
      this.formGroup.controls.cardNumber.hasError('min') ? 'Title must be 16 characters' : 
      this.formGroup.controls.cardNumber.hasError('max') ? 'Title must be 16 characters' :'';
  }
}
