import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MemebersService } from 'src/app/_services/memebers.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members: Member[];

  constructor(private membersService: MemebersService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers(){
    this.membersService.getMembers().subscribe(members => {
      this.members = members;
    })
  }
}
