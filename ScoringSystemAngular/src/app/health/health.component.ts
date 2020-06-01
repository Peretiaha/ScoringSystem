import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Health } from 'src/models/Health';
import { User } from 'src/models/User';

@Component({
  selector: 'app-health',
  templateUrl: './health.component.html',
  styleUrls: ['./health.component.scss']
})
export class HealthComponent implements OnInit {

  user: User = new User();
  userId: number;
  formGroup: FormGroup;
  health: Health = new Health();
  userName: string;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private popUp: MatSnackBar,
    private router: Router) {
    this.createForm();
  }

  ngOnInit(): void {
    const id = 'id';
    this.route.params.subscribe(params => this.userId = params[id]);
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    this.userService.getUserProfile().subscribe(x => {
      this.userName = x.name;
    });
  }

  createForm() {
    this.formGroup = new FormGroup({
      weight: new FormControl('', [Validators.required]),
      arterialPressure: new FormControl('', [Validators.required]),
      numberOfRespiratoryMovements: new FormControl('', [Validators.required]),
      heartRate: new FormControl('', [Validators.required]),
      hemoglobin: new FormControl('', [Validators.required]),
      weighBilirubint: new FormControl('', [Validators.required]),
      bloodSugar: new FormControl('', [Validators.required]),
      whiteBloodCells: new FormControl('', [Validators.required]),
      bodyTemperature: new FormControl('', [Validators.required]),
      bilirubin: new FormControl('', [Validators.required]),
    });
  }

  onCancelClick(): void {
  }

  submit(health: Health): void {
    if (this.formGroup.invalid) {
      this.formGroup.markAllAsTouched();
      return;
    }    

    this.userService.addHealthToUser(health, this.userId).subscribe(
        x => {
          this.popUp.open('Health successfully added to user data!', 'Ok',
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
}
