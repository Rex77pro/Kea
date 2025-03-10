import marimo

__generated_with = "0.11.17"
app = marimo.App(width="medium")


@app.cell
def _():
    import marimo as mo
    import matplotlib.pyplot as plt
    import pandas as pd
    import numpy as np
    from sklearn.model_selection import train_test_split
    return mo, np, pd, plt, train_test_split


@app.cell
def _(pd):
    df = pd.read_csv("housing.csv", sep=",")
    return (df,)


@app.cell
def _(df):
    df
    return


@app.cell
def _(df):
    df.info()
    return


@app.cell
def _(df):
    df["ocean_proximity"].value_counts()
    return


@app.cell
def _(df):
    df.describe()
    return


@app.cell
def _(df, plt):
    plt.figure(figsize=(12, 6))
    scatter = plt.scatter(
        df['longitude'], 
        df['latitude'],
        s=df['median_income'] * 50,  # Marker størrelse justeres efter median_income
        c=df['median_house_value'],  # Farvekodning for huspriser
        cmap='viridis',
        alpha=0.7
    )
    plt.xlabel('Longitude')
    plt.ylabel('Latitude')
    plt.title('Huspriser og indkomst fordelt over geografiske koordinater')
    cbar = plt.colorbar(scatter)
    cbar.set_label('Median House Value')
    plt.grid(True)
    plt.show()
    return cbar, scatter


@app.cell
def _(df):
    df.head()
    return


@app.cell
def _(df, plt):
    df.hist(bins=50,figsize=(12,8))
    plt.show()
    return


@app.cell
def _(df, train_test_split):
    train_set, test_set = train_test_split(df, test_size=0.2, random_state=42)
    print(len(train_set))
    print(len(test_set))
    return test_set, train_set


@app.cell
def _(df, np, pd):
    df["income_cat"] = pd.cut(df["median_income"],
                             bins=[0., 1.5, 3.0, 4.5, 6., np.inf],
                             labels=[1,2,3,4,5])
    return


@app.cell
def _(df, plt):
    df["income_cat"].value_counts().sort_index().plot.bar(rot=0, grid=True)
    plt.xlabel("Income category")
    plt.ylabel("Number of districts")
    plt.show()
    return


@app.cell
def _(df, train_test_split):
    strat_train_set, strat_test_set = train_test_split(df, test_size=0.2, stratify=df["income_cat"], random_state=42)
    strat_test_set["income_cat"].value_counts() / len(strat_test_set)
    return strat_test_set, strat_train_set


@app.cell
def _(df, plt):
    df.plot(kind="scatter",x="longitude",y="latitude",grid=True)
    plt.show()
    return


if __name__ == "__main__":
    app.run()
