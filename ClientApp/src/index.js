import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import Popup from 'reactjs-popup';

ReactDOM.render(<App />, document.getElementById('root'));

class GlossaryForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {terms: [], term: '', definition: 'Undefined', open: false};
        this.handleTermChange = this.handleTermChange.bind(this);
        this.handleDefinitionChange = this.handleDefinitionChange.bind(this);
        this.OnUpdateTerm = this.OnUpdateTerm.bind(this);
        this.OnUpdateDefinition = this.OnUpdateDefinition.bind(this);
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
        this.url = 'https://localhost:5001/api/Glossary';
    }
    componentDidMount() {
        fetch(this.url + "/GetTerms")
        .then(res => res.json())
        .then((data) => {
            this.setState({ terms: data });
        }).catch(console.log);
    }
    openModal() {
        this.setState({ open: true })
    }
    closeModal() {
        this.setState({ open: false })
    }
    handleTermChange(event) {
        this.setState({term: event.target.value})
    }
    handleDefinitionChange(event) {
        this.setState({definition: event.target.value})
    }
    OnUpdateTerm(event) {
        this.setState({term: event.target.value});
        var url_ = new URL(this.url + "/GetDefinition"),
            params = {term: this.state.term};
        Object.keys(params).forEach(key => url_.searchParams.append(key, params[key]))
        fetch(url_)
        .then(res => res.text())
        .then((data) => {
            this.setState({ definition: data });
        }).catch(console.log);
    }
    OnUpdateDefinition(event) {
        this.setState({definition: event.target.value});
        var url_ = new URL(this.url + "/UpdateTerm"),
            params = {term: this.state.term, definition: this.state.definition};
        Object.keys(params).forEach(key => url_.searchParams.append(key, params[key]));
        fetch(url_, {
            method: 'PUT',
        })
        .then(res => res.text())
        .then((data) => {
            if (data == 0) {
                this.openModal();
            }
            console.log(data); // @todo
        }).catch(console.log);        
    }
    render() {
        return (
            <div className="grid-container">
                <div className="content">
                    <input type="text" placeholder="Enter term here" value={this.state.term} onChange={this.handleTermChange} onBlur={this.OnUpdateTerm}/>
                </div>
                <div className="results">
                    <div className="results-div1">
                        <div className="results-div2">
                            <div className="results-div3">
                                <input type="text" placeholder="Enter definition here" className="term" value={this.state.definition} onChange={this.handleDefinitionChange} onBlur={this.OnUpdateDefinition} />
                            </div>
                        </div>
                    </div>
                </div>
            <Popup
              open={this.state.open}
              onClose={this.closeModal}
              position="right center"
              closeOnDocumentClick>
              <span class="update-message"><center>Update Success</center> </span>
            </Popup>                
            </div>
        );
    }
}

ReactDOM.render(<GlossaryForm />, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();