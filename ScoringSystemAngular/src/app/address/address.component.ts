import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Address } from 'src/models/Address';
import { User } from 'src/models/User';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {

  user: User = new User();
  userId: number;
  formGroup: FormGroup;
  address = new Address();
  userName: string;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private popUp: MatSnackBar) {
    this.createForm();
  }

  ngOnInit(): void {
    const id = 'id';
    this.route.params.subscribe(params => this.userId = params[id]);
    this.userService.getUserById(this.userId).subscribe(x => {
      if (x.address != null) {
        this.address = x.address;
      }
      this.userName = x.name;
    });
  }

  createForm() {
    this.formGroup = new FormGroup({
      addressLine1: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(30)]),
      addressLine2: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(30)]),
      postCode: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(8)]),
      country: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]),
      city: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]),
      stateOrProvince: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]),
    });
  }

  onCancelClick(): void {
    console.log(this.user);
  }

  submit(address: Address): void {
    if (this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }
    if (address.addressId == null) {
      this.userService.addAddressToUser(address, this.userId).subscribe(
        x => {
          this.popUp.open('Address successfully added to user data!', 'Ok',
            { duration: 2000, horizontalPosition: 'end', verticalPosition: 'top' });
        },
        error => {
          const formControl = this.formGroup.controls.name;
          formControl.setErrors({
            isbnError: error
          });
        }
      );
    }
    else {
      this.userService.editUserAddress(address).subscribe(
        x => {
          this.popUp.open(this.userName + '`s adderss edited successfully!', 'Ok',
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

  getAddress1ErrorMessage() {
    const nameErrors = this.formGroup.get('addressLine1').errors;
    return nameErrors.required ? 'AddressLine1 is required' :
          nameErrors.minlength ? 'AddressLine1 must be at least 10 characters long' :
          nameErrors.maxLength ? 'AddressLine1 must be no more than 30 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
  }

  getAddress2ErrorMessage() {
    const nameErrors = this.formGroup.get('addressLine2').errors;
    return nameErrors.required ? 'AddressLine2 is required' :
          nameErrors.minlength ? 'AddressLine2 must be at least 10 characters long' :
          nameErrors.maxLength ? 'AddressLine2 must be no more than 30 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
  }

  getCountryErrorMessage() {
    const nameErrors = this.formGroup.get('country').errors;
    return nameErrors.required ? 'Country is required' :
          nameErrors.minlength ? 'Country must be at least 3 characters long' :
          nameErrors.maxLength ? 'country must be no more than 20 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
  }

  getCityErrorMessage() {
    const nameErrors = this.formGroup.get('city').errors;
    return nameErrors.required ? 'City is required' :
          nameErrors.minlength ? 'City must be at least 3 characters long' :
          nameErrors.maxLength ? 'City must be no more than 20 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
  }

  getStateOrProvinceErrorMessage() {
    const nameErrors = this.formGroup.get('stateOrProvince').errors;
    return nameErrors.required ? 'State Or Province is required' :
          nameErrors.minlength ? 'State Or Province must be at least 3 characters long' :
          nameErrors.maxLength ? 'State Or Province must be no more than 20 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
  }

  getPostCodeErrorMessage() {
    const nameErrors = this.formGroup.get('postCode').errors;
    return nameErrors.required ? 'postCode is required' :
          nameErrors.minlength ? 'Post Code must be at least 5 characters long' :
          nameErrors.maxLength ? 'Post Code must be no more than 8 characters long' :
          !!nameErrors.serverErrors ? nameErrors.serverErrors :
        null;
  }
}
