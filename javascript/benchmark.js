let _benchmark = function () {
    let _startDate = null;
    let _endDate = null;
    let _duration = null;
    let _message = "";

    this.start = (message) => {
        _startDate = new Date();
        _endDate = null;
        _duration = null;

        if (message) {
            _message = message;
            console.log(_message);
        }

        return this;
    };

    this.stop = () => {
        _endDate = new Date();
        _duration = _endDate.getTime() - _startDate.getTime();

        this.show();

        return this;
    };

    this.show = () => {
        if (_startDate == null) {
            console.log("No iniciado");
            return this;
        }

        if (_endDate == null) {
            console.log("Inicio: ", _startDate, " No finalizado");
            return this;
        }

        let log = "";
        if (_message && _message != "")
            log += "Fin: " + _message + ", ";
        //console.log("Inicio: ", _startDate, " Fin: ", _endDate);

        log += "Duración (ms): " + _duration;

        console.log(log);

        return this;
    };
};
