using UnityEngine;
using System.Collections.Generic;

public class CustomArrayList<T> : object   {

	private T[] contents;
	private int size = 0;
	private int max = 20;
	
	public IEnumerator<T> GetEnumerator ()
	{
		if (contents != null)
		{
			for (int i = 0; i < size; ++i)
			{
				yield return contents[i];
			}
		}
	}
	
	public CustomArrayList() {
		this.contents = new T[this.max];
	}
	
	public CustomArrayList(int max) {
		this.max = max;
		this.contents = new T[this.max];
	}
	
	private void Increase_Capacity() {
		this.max *= 2;
		T[] old = this.contents;
		this.contents = new T[this.max];
		for(int i = 0; i < this.size; i++)
			this.contents[i] = old[i];
	}
	
	public void Add(T item) {
		if(this.size == this.max) Increase_Capacity();
		
		this.contents[size] = item;
		size++;
	}
	
	public bool Contains(T item) {
		return Search(item) != -1;
	}
	
	
	public int Search(T item) {
		for(int i = 0; i < this.size; i++){
			if(this.contents[i].Equals(item)) return i;
		}
		
		return -1;
	}
	
	public void Remove(T item) {
		int index = Search(item);
		
		if(index == -1) return;
		
		if(index != this.size){
			for(int i = index; i < this.size - 1; i++)
				this.contents[i] = this.contents[i + 1];
		}
		
		this.size--;
			
	}
	
	public bool Set(int index, T item){
		if(index < 0 || index >= size) return false;
		
		this.contents[index] = item;
		
		return true;
	}
	
	public bool Swap(int index1, int index2){
		if(index1 < 0 || index1 >= size) return false;
		if(index2 < 0 || index2 >= size) return false;
		
		T holder = this.contents[index1];
		this.contents[index1] = this.contents[index2];
		this.contents[index2] = holder;
		
		return true;
	}
	
	public T Get(int index) {
		return this.contents[index];
	}
	
	public T[] Get_Contents(){
		return this.contents;	
	}
	
	public int Get_Size() {
		return this.size;	
	}
	
	public void Clear() {
		this.size = 0;	
	}
}
