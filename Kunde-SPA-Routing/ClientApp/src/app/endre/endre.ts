import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Kunde } from "../Kunde";

@Component({
  templateUrl: "endre.html"
})
export class Endre {
  skjema: FormGroup;
  
  validering = {
    id: [""],
    fornavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    etternavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    adresse: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    postnr: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])
    ],
    poststed: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ]
  }

  constructor(private http: HttpClient, private fb: FormBuilder,
              private route: ActivatedRoute, private router: Router) {
      this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.endreKunde(params.id);
    })
  }

  vedSubmit() {
      this.endreEnKunde();
  }

  endreKunde(id: number) {
    this.http.get<Kunde>("api/kunde/" + id)
      .subscribe(
        kunde => {
          this.skjema.patchValue({ id: kunde.id });
          this.skjema.patchValue({ fornavn: kunde.fornavn });
          this.skjema.patchValue({ etternavn: kunde.etternavn });
          this.skjema.patchValue({ adresse: kunde.adresse });
          this.skjema.patchValue({ postnr: kunde.postnr });
          this.skjema.patchValue({ poststed: kunde.poststed });
        },
        error => console.log(error)
      );
  }

  endreEnKunde() {
    const endretKunde = new Kunde();
    endretKunde.id = this.skjema.value.id;
    endretKunde.fornavn = this.skjema.value.fornavn;
    endretKunde.etternavn = this.skjema.value.etternavn;
    endretKunde.adresse = this.skjema.value.adresse;
    endretKunde.postnr = this.skjema.value.postnr;
    endretKunde.poststed = this.skjema.value.poststed;

    this.http.put("api/kunde/", endretKunde)
      .subscribe(
        retur => {
          this.router.navigate(['/liste']);
        },
        error => console.log(error)
      );
  }
}
