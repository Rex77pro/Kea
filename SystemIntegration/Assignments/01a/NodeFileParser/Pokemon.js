class Pokemon {
    constructor({ name, nationalNo, types}) {
        this.nationalNo = nationalNo;
        this.name = name;
        this.types = types;
    }

    toString() {
        return `Pokemon: (#${this.nationalNo}), ${this.name}, ${this.types.join(', ')} `
    }
}

module.exports = Pokemon;