#[no_mangle]
pub extern fn add_one(x: u32) -> u32 {
    x + 1
}
// to use in browser build it with cargo build --target wasm32-unknown-unknown --release
//inspired by https://egghead.io/lessons/javascript-load-a-webassembly-function-written-in-rust-and-invoke-it-from-javascript
// to execute directly, call it with cargo run --release

 
//leetcode problem #878
pub struct Solution{}
impl Solution {
    pub fn nth_magical_number(n: i32, a: i32, b: i32) -> i32 {
        let ab = Solution::get_ab(&a,&b);
        let modulo = 1000_000_007;
        let mut result = 0;
        let count_step = ((ab / (a as i64)) + (ab / (b as i64)) - 1) as i32;
        let mut mult_step = 1;
        let mut current_count = 0;

        loop {
            let new_current_count = current_count + count_step * mult_step;
            let new_result = (result +((ab*(mult_step as i64))%modulo))%modulo;

            if new_current_count == n {
                return new_result as i32;
            }
            if new_current_count > n {
                if mult_step > 1 {
                    mult_step = 1;
                    continue;
                }else {
                    break;
                }
            }

            mult_step *= 2;
            current_count = new_current_count;
            result = new_result;
        }


        let mut path_a: i64 = 0;
        let mut path_b: i64 = 0;
        let mut result_a = result;
        let mut result_b = result;
        let mut last_was_a = false;

        while current_count < n {
            if path_a +(a as i64) < path_b + (b as i64) {
                result_a = (result_a + (a as i64)) % modulo;
                path_a += a as i64;
                last_was_a = true;
            }else{
                result_b = (result_b + (b as i64)) % modulo;
                path_b += b as i64;
                last_was_a = false;
            }

            current_count += 1;
        }
        
        if last_was_a {
            return result_a as i32;
        }else{
            return result_b as i32;
        }
    }

    fn get_primes() -> Vec<i32> {
        let n = 40_001;
        let mut all_numbers = vec![1; n];
        all_numbers[0] = 0;
        all_numbers[1] = 0;
        for i in 2..n {
            if all_numbers[i] == 1 {
                let mut j = i * 2;
                while j < n {
                    all_numbers[j] = 0;
                    j += i;
                }
            }
        }

        let mut result = vec![];
        for i in 2..n {
            if all_numbers[i] == 1 {
                result.push(i as i32);
            }
        }
        return result;
    }

    fn get_prime_components(num: &i32, primes: &Vec<i32>) -> Vec<i32> {
        let mut result: Vec<i32> = vec![];
        let mut x = *num;
        for i in 0..(primes.len() - 1) {
            let mut count = 0;
            let prime = primes[i];
            while (x % prime) == 0 {
                count += 1;
                x = x / prime;
            }
            result.push(count);
        }
        return result;
    }

    fn get_ab(a: &i32, b: &i32) -> i64 {
        let primes = Solution::get_primes();
        let a_components = Solution::get_prime_components(&a, &primes);
        let b_components = Solution::get_prime_components(&b, &primes);
        let mut ab: i64 = 1;
        for i in 0..(primes.len() -1)  {
            let mut count = std::cmp::max(a_components[i], b_components[i]);
            while count > 0 {
                ab = ab * (primes[i] as i64);
                count -= 1;
            }
        }

        return ab;
    }
}

#[no_mangle]
pub extern fn nth_magical_number(n: i32, a: i32, b: i32) -> i32 {
    Solution::nth_magical_number(n,a,b)
}


fn main(){
    
    use std::time::Instant;
    let now = Instant::now();
    let n = 1000000000;
    let a = 8888;
    for b in 9000..10000{
        let _calculation_result = nth_magical_number(n, a, b);
    }

    let elapsed = now.elapsed();
    println!("result is {:#?}", elapsed.as_millis());
}