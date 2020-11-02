import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Modal } from './sletteModal';
import { Kunde } from "../Kunde";

@Component({
  templateUrl: "liste.html"
})
export class Liste {
  alleKunder: Array<Kunde>;
  laster: boolean;
  kundeTilSletting: string;
  slettingOK: boolean;

  constructor(private http: HttpClient, private router: Router, private modalService: NgbModal) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleKunder();
  }

  hentAlleKunder() {
    this.http.get<Kunde[]>("api/kunde/")
      .subscribe(kundene => {
        this.alleKunder = kundene;
        this.laster = false;
      },
       error => console.log(error)
      );
  };

  // denne slette funksjonen blir litt komplisert da vi har to asynkrone kall inne i hverandre
  sletteKunde(id: number) {

    // først hent navnet på kunden
    this.http.get<Kunde>("api/kunde/" + id)
      .subscribe(kunde => {
        this.kundeTilSletting = kunde.fornavn + " " + kunde.etternavn;

        // så vis modalen og evt. kall til slett
        this.visModalOgSlett(id);
      },
        error => console.log(error)
      );
  }

  visModalOgSlett(id: number) {
    const modalRef = this.modalService.open(Modal);

    modalRef.componentInstance.navn = this.kundeTilSletting;

    modalRef.result.then(retur => {
        console.log('Lukket med:' + retur);
        if (retur == "Slett") {

          // kall til server for sletting
          this.http.delete("api/kunde/" + id)
            .subscribe(retur => {
              this.hentAlleKunder();
            },
              error => console.log(error)
            );
        }
        this.router.navigate(['/liste']);
     });
  }
}