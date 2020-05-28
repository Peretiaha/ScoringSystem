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
    this.userService.getUserById(this.userId).subscribe(x=>{
      this.address = x.address;
      this.userName = x.name;
    });
    console.log(this.address);
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
    if (address.addressId === 0) {
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
      x=> {
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
}
