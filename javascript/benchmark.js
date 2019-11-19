let _benchmark = new function (){
    let _startDate: null;
    let _endDate: null;
    let _duration: null;
    let _message: "";

    this.start: (message) => {
        _startDate = new Date();
        _endDate = null;
        _duration = null;

        if (message) _message = message;

        return this;
    }; 
	
    this.stop: () => {
        _endDate = new Date();
        _duration = _endDate.getTime() - _startDate.getTime();

        return this;
    };
	
    this.show: () => {
        if (_startDate == null) {
            console.log("No iniciado");
            return this;
        }

        if (_endDate == null) {
            console.log("Inicio: ", _startDate, " No finalizado");
            return this;
        }

        if (_message && _message != "")
            console.log(_message);
        console.log("Inicio: ", _startDate, " Fin: ", _endDate);
        console.log("Duración (ms): ", _duration);

        return this;
    };
};
