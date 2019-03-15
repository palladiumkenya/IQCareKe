import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-indicator-results-grid',
  templateUrl: './indicator-results-grid.component.html',
  styleUrls: ['./indicator-results-grid.component.css']
})
export class IndicatorResultsGridComponent implements OnInit {

  @Input('IndicatorResults')  IndicatorResults : any[];
  filteredIndicatorResults : any[] = [];
  @Input('SubSectionId')     SubSectionId : number;

  indicator_results_displaycolumns : any[] = ['code','name','result'];

  indicatorResultsDataSource = new MatTableDataSource(this.IndicatorResults);
  @ViewChild(MatPaginator) paginator : MatPaginator;

  constructor() { 

  }

  ngOnInit() 
  {
    this.indicatorResultsDataSource = new MatTableDataSource(this.IndicatorResults);
    this.indicatorResultsDataSource.paginator = this.paginator;
  }

  
      
  }
  
