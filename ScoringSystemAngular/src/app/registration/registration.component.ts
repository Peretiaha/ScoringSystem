import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RegisterUser } from 'src/models/RegisterUser';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  @Output() public onUploadFinished = new EventEmitter();
  user: RegisterUser = new RegisterUser();
  formGroup: FormGroup;

  constructor(
    private userService: UserService,
    private popUp: MatSnackBar,
    public translate: TranslateService) {      
    this.createForm();
  }
  ngOnInit(): void {
  }

  createForm() {
    this.formGroup = new FormGroup({
      name: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      birthday: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required]),
      photo: new FormControl('')
    });
  }

  onCancelClick(): void {

  }

  submit(user: RegisterUser, files: any): void {
    if (this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.userService.createUser(user).subscribe(
      x => {
        if (x != 0) {
          this.uploadFile(files, <number>x);
          this.popUp.open('User registered successfully!', 'Ok',
            { duration: 2000, horizontalPosition: 'end', verticalPosition: 'top' });
        }
      },
      error => {
        const formControl = this.formGroup.controls.name;
        formControl.setErrors({
          isbnError: error
        });
      }
    );
  }

  public uploadFile(files, userId: number) {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.userService.uploadImage(formData, userId);
  }

}
