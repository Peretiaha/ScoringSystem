import { Component, OnInit } from '@angular/core';
import { UserChangeRole } from 'src/models/user-change-role';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';
import { Role } from 'src/models/role';
import { UserService } from 'src/services/user.service';
import { RoleService } from 'src/services/role.service';
import { FormGroup, FormControl } from '@angular/forms';
import { ChangeRoles } from 'src/models/changeRoles';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  private id: number;
  public dataSource = new MatTableDataSource<UserChangeRole>();
  public displayedColumns: string[] = ['name', 'lastName', 'email','roles', 'drop'];
  public roles: Array<Role>;
  public selectedRoles: Array<string>;
  formGroup: FormGroup;


  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private snackbar: MatSnackBar,
              public createDialog: MatDialog,
              public roleService: RoleService
  ) {
    this.createForm();
   }

  ngOnInit(): void {
    const id = 'id';
    this.route.params.subscribe(params => this.id = params[id]);
    this.userService.fetchCustomerUsers().subscribe(x => this.dataSource = new MatTableDataSource(x));
    this.roleService.fetchRoles().subscribe(x=>this.roles=x);
  }

  createForm() {
    this.formGroup = new FormGroup({
      roles: new FormControl('',)
    });
  }

  onSelect(userId: number){
    if (userId != null && this.selectedRoles.length != null){
      var changeRoles = new ChangeRoles();
        changeRoles.userId = userId;
        changeRoles.roles = this.selectedRoles;
      this.userService.changeCustomerRole(changeRoles).subscribe(x=> this.ngOnInit());
      this.selectedRoles = null;
    }
  }
}
