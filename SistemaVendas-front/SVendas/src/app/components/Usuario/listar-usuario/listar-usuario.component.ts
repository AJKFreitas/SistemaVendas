import { DialogBoxComponent } from './../../Shared/dialog-box/dialog-box.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export interface UsersData {
  name: string;
  id: number;
}

const ELEMENT_DATA: UsersData[] = [
  { id: 1560608769632, name: 'Artificial Intelligence' },
  { id: 1560608796014, name: 'Machine Learning' },
  { id: 1560608787815, name: 'Robotic Process Automation' },
  { id: 1560608769632, name: 'Artificial Intelligence' },
  { id: 1560608796014, name: 'Machine Learning' },
  { id: 1560608787815, name: 'Robotic Process Automation' },
  { id: 1560608769632, name: 'Artificial Intelligence' },
  { id: 1560608796014, name: 'Machine Learning' },
  { id: 1560608787815, name: 'Robotic Process Automation' },
  { id: 1560608805101, name: 'Blockchain' }
];

@Component({
  selector: 'app-listar-usuario',
  templateUrl: './listar-usuario.component.html',
  styleUrls: ['./listar-usuario.component.css']
})
export class ListarUsuarioComponent implements OnInit {
  dataSource: MatTableDataSource<UsersData>;
  displayedColumns: string[] = ['id', 'name', 'action'];
  
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  
  constructor(public dialog: MatDialog) {}
  
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(ELEMENT_DATA);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '550px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event === 'Add') {
        this.addRowData(result.data);
      } else if (result.event === 'Update') {
        this.updateRowData(result.data);
      } else if (result.event === 'Delete') {
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(row_obj) {
    const d = new Date();
    this.dataSource.data.push({
          id: d.getTime(),
          name: row_obj.name
        });
    this.table.renderRows();
  }
  updateRowData(row_obj) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === row_obj.id) {
        value.name = row_obj.name;
      }
      return true;
    });
  }
  deleteRowData(row_obj) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== row_obj.id;
    });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}

