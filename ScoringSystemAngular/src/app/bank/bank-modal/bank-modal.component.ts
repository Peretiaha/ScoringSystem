import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';
import { BankService } from 'src/services/bank.service';
import { BankDialogData } from '../../../models/bank-dialog-model';
import { Bank } from '../../../models/bank';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-bank-modal',
  templateUrl: './bank-modal.component.html',
  styleUrls: ['./bank-modal.component.scss']
})
export class BankModalComponent implements OnInit {

  bank: Bank = new Bank();
  id: number;
  formGroup: FormGroup;

  constructor(public dialogRef: MatDialogRef<BankModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: BankDialogData,
              private bankService: BankService,
              private popUp: MatSnackBar) {
    this.createForm();
  }

  ngOnInit() {
    if (this.data.bank !== undefined) {
      this.bank = { ... this.data.bank };
    } else {
      this.bank.bankId = 0;
    }
  }

  createForm() {
    this.formGroup = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]),
      linkToSite: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
    });
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  submit(bank: Bank): void {
    if (this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    if (bank.bankId === 0) {
      this.bankService.createBank(bank).subscribe(
        (respondBook: Bank) => {
          this.dialogRef.close();
          this.popUp.open('Bank created successfully!', 'Ok',
            { duration: 2000, horizontalPosition: 'end', verticalPosition: 'top' });
        },
        error => {
          const formControl = this.formGroup.controls.name;
          formControl.setErrors({
            isbnError: error
          });
        }
      );
    } else {
      this.bankService.editBank(bank).subscribe(
        (respondBook: Bank) => {
          this.dialogRef.close();
          this.popUp.open('Book edited successfully!', 'Ok',
            { duration: 2000, horizontalPosition: 'end', verticalPosition: 'top' });
        },
        error => {
          const formControl = this.formGroup.controls.isbn;
          formControl.setErrors({
            isbnError: error
          });
        }
      );
    }
  }

  getIsbnErrorMessage() {
    return this.formGroup.controls.isbn.hasError('required') ? 'Isbn is required' :
      this.formGroup.controls.isbn.hasError('min') ? 'Isbn must be 13 characters' :
        this.formGroup.controls.isbn.hasError('max') ? 'Isbn must be 13 characters' :
          !!this.formGroup.controls.isbn.errors.isbnError ? this.formGroup.controls.isbn.errors.isbnError : '';
  }

  getTitleErrorMessage() {
    return this.formGroup.controls.title.hasError('required') ? 'Title is required' :
      this.formGroup.controls.title.hasError('minlength') ? 'Title must be more than 3 characters' : '';
  }

  getAuthorErrorMessage() {
    return this.formGroup.controls.author.hasError('required') ? 'Author is required' :
      this.formGroup.controls.author.hasError('minlength') ? 'Author must be more than 3 characters' : '';
  }
}
