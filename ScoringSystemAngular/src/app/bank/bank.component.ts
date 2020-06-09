import { Component, OnInit } from '@angular/core';
import {BankService} from '../../services/bank.service'
import { ActivatedRoute } from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Bank } from '../../models/bank';
import { SelectionModel } from '@angular/cdk/collections';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';
import { BankModalComponent } from './bank-modal/bank-modal.component';


@Component({
  selector: 'app-bank',
  templateUrl: './bank.component.html',
  styleUrls: ['./bank.component.scss']
})
export class BankComponent implements OnInit {

  private id: number;
  public dataSource = new MatTableDataSource<Bank>();
  public displayedColumns: string[] = ['select', 'name', 'linkToSite', 'delete'];
  public selection = new SelectionModel<Bank>(true, []);

  constructor(private bankService: BankService,
              private route: ActivatedRoute,
              private snackbar: MatSnackBar,
              public createDialog: MatDialog
  ) { }

  ngOnInit(): void {
    const id = 'id';
    this.route.params.subscribe(params => this.id = params[id]);
    this.bankService.fetchBanks().subscribe(x => this.dataSource = new MatTableDataSource(x));
  }

  openCreateModal() {
    const dialogRef = this.createDialog.open(BankModalComponent, {
      width: '500px',
      data: { action: '+ Create New Bank' },
      panelClass: 'custom-dialog-container' 
    });

    dialogRef.afterClosed().subscribe(() =>
        this.bankService.fetchBanks()
        .subscribe(books => this.dataSource = new MatTableDataSource(books)));
  }

  onEditModule(bank: Bank) {
    const dialogRef = this.createDialog.open(BankModalComponent, {
      width: '500px',
      panelClass: 'custom-dialog-container' ,
      data: { action: 'Edit', collinsListId: this.id, bank }
    });

    dialogRef.afterClosed().subscribe(x =>
      this.bankService.fetchBanks()
      .subscribe(resBook => this.dataSource = new MatTableDataSource(resBook)));
  }

  onSelectDelete() {
    if (this.selection.selected.length === 0) {
      this.showInfoWindow('Nothing to delete');
    } else {
      const dialogRef = this.createDialog.open(DeleteModalComponent, {
        panelClass: 'custom-dialog-container',
        width: '500px'
      });
      dialogRef.afterClosed().subscribe(x => {
        if (x.data === true) {
          const booksIds = this.getSelectedBanks();
          booksIds.forEach(element => {
            this.bankService.deleteById(element).subscribe(res => {
              this.bankService.fetchBanks().subscribe(books => this.dataSource = new MatTableDataSource(books));
              this.showInfoWindow('Selected books deleted successful');
              this.selection.clear();
            });
          });
        }
      });
    }
  }

  onDelete(bookId: number) {
    const dialogRef = this.createDialog.open(DeleteModalComponent, {
      panelClass: 'custom-dialog-container', 
      width: '500px'
    });
    dialogRef.afterClosed().subscribe(x => {
      if (x !== undefined && x.data === true) {
        this.bankService.deleteById(bookId).subscribe(res => {
          this.bankService.fetchBanks().subscribe(books => this.dataSource = new MatTableDataSource(books));
          this.showInfoWindow('Delete successful');
        });
      }
    });
  }

  showInfoWindow(text: string) {
    this.snackbar.open(text, 'ok', {
      duration: 1000,
      verticalPosition: 'top'
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  checkboxLabel(row?: Bank): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.name + 1}`;
  }

  getSelectedBanks(): number[] {
    return this.selection.selected.map(x => x.bankId);
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }
}
