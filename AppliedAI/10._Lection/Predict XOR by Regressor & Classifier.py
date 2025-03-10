import marimo

__generated_with = "0.11.12"
app = marimo.App(width="medium")


@app.cell
def _():
    return


@app.cell
def _():
    import numpy as np
    return (np,)


@app.cell
def _():
    from sklearn.neural_network import MLPClassifier
    from sklearn.neural_network import MLPRegressor
    return MLPClassifier, MLPRegressor


@app.cell
def _(np):
    X = np.array([[0,0], [0,1], [1, 0], [1, 1]])
    y = np.array([0, 1, 1, 0])
    return X, y


@app.cell
def _(MLPClassifier, X, y):
    mlpc = MLPClassifier(hidden_layer_sizes=(100,), max_iter=2000) 
    mlpc.fit(X, y)
    return (mlpc,)


@app.cell
def _(MLPRegressor, X, y):
    mlpr = MLPRegressor(hidden_layer_sizes=(100,), max_iter=2000)
    mlpr.fit(X, y)
    return (mlpr,)


@app.cell
def _(X, mlpc):
    mlpc.predict(X)
    return


@app.cell
def _(X, mlpr):
    mlpr.predict(X)
    return


if __name__ == "__main__":
    app.run()
