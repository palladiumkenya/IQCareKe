import { PnsService } from './../../_services/pns.service';
import { Observable } from 'rxjs/Observable';
import { DataSource } from '@angular/cdk/collections';
import { Component, OnInit, NgZone } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-pns-tracing-list',
	templateUrl: './pns-tracing-list.component.html',
	styleUrls: ['./pns-tracing-list.component.css']
})
export class PnsTracingListComponent implements OnInit {
	personId: number;
	displayedColumns = ['tracingDate', 'tracingMode', 'tracingOutcome', 'consent', 'dateBookedTesting'];
	dataSource = new PnsTracingListDataSource(this.pnsService, this.personId);

	constructor(private pnsService: PnsService,
		private router: Router,
		private route: ActivatedRoute,
		public zone: NgZone) { }

	ngOnInit() {
		this.personId = JSON.parse(localStorage.getItem('partnerId'));

		this.dataSource = new PnsTracingListDataSource(this.pnsService, this.personId);
	}

	newTracing() {
		this.zone.run(() => { this.router.navigate(['/hts/pns/pnstracing'], { relativeTo: this.route }); });
	}
}

export class PnsTracingListDataSource extends DataSource<any> {
	constructor(private pnsService: PnsService, private personId: number) {
		super();
	}

	connect(): Observable<any[]> {
		return this.pnsService.geTracingList(this.personId);
	}

	disconnect() { }
}
